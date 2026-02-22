import React, { useEffect, useState, useRef } from 'react';
import ReactDOM from 'react-dom';
import { useNotifications } from '../../services/Notification';
import { formatDate } from '../../services/DateFormat';

export function NotificationIcon() {
    const currentUser = JSON.parse(localStorage.getItem('currentUser'));
    const { notifications, loadNotifications, markAsRead } = useNotifications(currentUser?.id);
    const unreadCount = notifications.filter(n => !n.isRead).length;
    const [isOpen, setIsOpen] = useState(false);
    const [position, setPosition] = useState({ top: 0, right: 0 });
    const buttonRef = useRef(null);
    const wrapperRef = useRef(null);
    const menuRef = useRef(null); // реф для меню

    useEffect(() => {
        loadNotifications();
    }, [loadNotifications]);

    // Закрытие при клике вне компонента и меню
    useEffect(() => {
        const handleClickOutside = (event) => {
            // Если клик был на кнопке - игнорируем (кнопка сама обрабатывает)
            if (buttonRef.current && buttonRef.current.contains(event.target)) return;
            // Если клик был внутри меню - тоже игнорируем
            if (menuRef.current && menuRef.current.contains(event.target)) return;
            // Иначе закрываем
            setIsOpen(false);
        };
        document.addEventListener('mousedown', handleClickOutside);
        return () => document.removeEventListener('mousedown', handleClickOutside);
    }, []);

    // Вычисляем позицию при открытии
    useEffect(() => {
        if (isOpen && buttonRef.current) {
            const rect = buttonRef.current.getBoundingClientRect();
            setPosition({
                top: rect.bottom + window.scrollY,
                right: window.innerWidth - rect.right,
            });
        }
    }, [isOpen]);

    const handleBellClick = () => setIsOpen(prev => !prev);

    const handleNotificationClick = (notification) => {
        if (!notification.isRead) {
            markAsRead(notification.id);
        }
        setIsOpen(false);
        if (notification.taskId) {
            // navigate(`/tasks/${notification.taskId}`);
        }
    };

    return (
        <div className="notification-wrapper" ref={wrapperRef}>
            {currentUser && (
                <>
                    <button ref={buttonRef} className="bell-icon" onClick={handleBellClick}>
                        🔔
                        {unreadCount > 0 && <span className="badge">{unreadCount}</span>}
                    </button>
                    {isOpen && ReactDOM.createPortal(
                        <div
                            ref={menuRef}
                            className="dropdown-content"
                            style={{
                                position: 'absolute',
                                top: position.top,
                                right: position.right,
                                width: '320px',
                                maxHeight: '400px',
                                overflowY: 'auto',
                                background: 'white',
                                borderRadius: '12px',
                                boxShadow: '0 10px 30px rgba(0,0,0,0.15)',
                                zIndex: 9999,
                                padding: '8px 0',
                                marginTop: '8px',
                                animation: 'slideDown 0.2s ease',
                            }}
                        >
                            {notifications.length === 0 ? (
                                <div className="empty-state">✨ Нет уведомлений</div>
                            ) : (
                                notifications.map((notification) => (
                                    <div
                                        key={notification.id}
                                        className={`notification-item ${!notification.isRead ? 'unread' : ''}`}
                                        onClick={() => handleNotificationClick(notification)}
                                    >
                                        <div className="notification-content">
                                            <div className="notification-message">{notification.message}</div>
                                            <div className="notification-time">
                                                {formatDate(notification.createdAt)}
                                            </div>
                                        </div>
                                        {!notification.isRead && <span className="unread-dot" />}
                                    </div>
                                ))
                            )}
                        </div>,
                        document.body
                    )}
                </>
            )}
        </div>
    );
}