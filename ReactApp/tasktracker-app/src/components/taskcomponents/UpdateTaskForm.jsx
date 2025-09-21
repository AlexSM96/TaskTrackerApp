import { useState, useRef } from 'react'
import { Input, Button, Textarea, NativeSelect } from '@chakra-ui/react' 


export default function UpdateTaskForm ({task, users, onUpdate }){
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
                <Input
                    placeholder="Тема задачи"
                    required={true}
                    readOnly={updatedTask.executed}
                    value={updatedTask.title}
                    onChange={(e) => setTask({ ...updatedTask, title: e.target.value })}
                />
                <Textarea
                    placeholder="Описание"
                    required={true}
                    readOnly={updatedTask.executed}
                    value={updatedTask.description}
                    onChange={(e) => setTask({...updatedTask, description: e.target.value})}
                />
                <NativeSelect.Root
                    value={task.executor?.id ?? undefined}
                    disabled={task.executed}
                    onChange={(event) => setTask({...updatedTask, executorId: event.target.value })}
                >
                    <NativeSelect.Field>
                        <option value={undefined}>Выбрать исполнителя</option>
                        {users.map(u => <option key={u.id} value={u.id}>{u.username}</option>)}
                    </NativeSelect.Field>
                    <NativeSelect.Indicator />
                </NativeSelect.Root>
                <Button
                    className="flex w-full justify-center rounded-md bg-indigo-600 px-3 py-1.5 text-sm/6 font-semibold text-white shadow-xs hover:bg-indigo-500 focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600"
                    type="submit"
                    disabled={task.executed}
                >
                    Обновить
                </Button>
            </form>  
       </div>
    );
}