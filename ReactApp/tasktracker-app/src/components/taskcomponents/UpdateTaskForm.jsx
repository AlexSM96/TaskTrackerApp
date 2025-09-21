import { useState, useRef } from 'react'
import { Input, Button, Textarea, NativeSelect } from '@chakra-ui/react' 


export default function UpdateTaskForm ({task, users, onUpdate}){
    const errRef = useRef()
    const [errMsg, setErrMsg] = useState('')
    const [updatedTask, setTask] = useState(task)
    
    const onSubmit = async (event) => {
        event.preventDefault()
        setErrMsg('')

        try {
            await onUpdate(updatedTask)
        } catch (err) {
            if (!err?.response) {
                setErrMsg('Сервер не отвечает')
            } else if (err.response.status === 401) {
                setErrMsg('Вы не авторизованы')
            } else {
                setErrMsg('Не удалось обновить задачу')
            }
        }
    }

    return (
       <div>
            <form className="w-full flex flex-col gap-3" onSubmit={onSubmit}>
                <h3 className="font-bold text-xl">Обновить задачу</h3>
                <p
                    ref={errRef}
                    className={errMsg ? "dark:text-red-400" : "offscreen"}
                    aria-live="assertive"
                >
                    {errMsg}
                </p>
                <label>Тема задачи</label>
                <Input
                    placeholder="Тема задачи"
                    required={true}
                    readOnly={updatedTask.taskWorkStatus == 2}
                    value={updatedTask.title}
                    onChange={(e) => setTask({ ...updatedTask, title: e.target.value })}
                />
                <label>Описание</label>
                <Textarea
                    placeholder="Описание"
                    required={true}
                    readOnly={updatedTask.taskWorkStatus == 2}
                    value={updatedTask.description}
                    onChange={(e) => setTask({...updatedTask, description: e.target.value})}
                />
                <label>Исполнитель</label>
                <NativeSelect.Root
                    disabled={task.taskWorkStatus == 2}
                    onChange={(event) => setTask({...updatedTask, executorId: event.target.value })}
                >
                    <NativeSelect.Field value={updatedTask.executor?.id}>
                        <option value={''}>Выбрать исполнителя</option>
                        {users?.map(u => <option key={u.id} value={u.id}>{u.fio}</option>)}
                    </NativeSelect.Field>
                    <NativeSelect.Indicator />
                </NativeSelect.Root>
                <Button
                    className="flex w-full justify-center rounded-md bg-indigo-600 px-3 py-1.5 text-sm/6 font-semibold text-white shadow-xs hover:bg-indigo-500 focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600"
                    type="submit"
                    disabled={task.taskWorkStatus == 2}
                >
                    Обновить
                </Button>
            </form>  
       </div>
    );
}