import { useEffect, useState, useCallback } from 'react';
import * as signalR from '@microsoft/signalr';
import {axiosInstance}  from "./AxiosWithAuthorization";


export function useNotifications(userId) {
    const [notifications, setNotifications] = useState([]);
    const [connection, setConnection] = useState(null);

    useEffect(() => {
        if (!userId) return;

        const newConnection = new signalR.HubConnectionBuilder()
            .withUrl(`http://localhost:5037/notifications?userId=${userId}`)
            .withAutomaticReconnect()
            .build();

        setConnection(newConnection);
    }, [userId]);

    useEffect(() => {
        if (!connection) return;

        connection.start()
            .then(() => {
                console.log('Connected to NotificationHub');
                connection.on('ReceiveNotification', (notification) => {
                    setNotifications(prev => [notification, ...prev]);
                    alert(notification.message);
                });
            })
            .catch(err => console.error('Connection failed: ', err));

        return () => {
            connection.off('ReceiveNotification');
            connection.stop();
        };
    }, [connection]);

     const loadNotifications = useCallback(async () => {
        try {
            const response = await axiosInstance.get(`/user_notifications/get?userId=${userId}`);
            const data = await response.data;
            setNotifications(data);
        } catch (error) {
            console.error('Failed to load notifications:', error);
        }
    }, [userId]); // зависимость от userId


    const markAsRead = useCallback(async (notificationId) => {
    try {
        await axiosInstance.post(`/user_notifications/read?notificationId=${notificationId}`);
        setNotifications(prev =>
            prev.map(n => n.id === notificationId ? { ...n, isRead: true } : n)
        );
    } catch (error) {
        console.error('Failed to mark as read:', error);
    }
    }, [userId]); 
       
    return { notifications, loadNotifications, markAsRead };
}