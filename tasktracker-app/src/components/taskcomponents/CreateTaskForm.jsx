import { Input, Button, Textarea, NativeSelect } from '@chakra-ui/react' 
import { useRef, useState, useEffect } from 'react'

export default function CreateTaskForm({users, onCreate}) {
    const errRef = useRef();
    const [errMsg, setErrMsg] = useState('');
    const [task, setTask] = useState(null)

    const onSubmit = (event) => {
        event.preventDefault();
        try{
            setTask(null)
            onCreate(task)
        }
        catch(err){
            if(!err?.response){
                setErrMsg('Сервер не отвечает')
              }else if(err.response?.status === 401){
                setErrMsg('Вы не зарегестрированы в системе')
              }else{
                setErrMsg('Не удалось создать задачу')
              }
        }
    }

    return (
        <form className='w-full flex flex-col gap-3' onSubmit={onSubmit}>
            <h3 className='font-bold text-x2'>Создать задачу</h3>
            <p ref={errRef} className={errMsg ? "dark:text-red-400" : "offscreen"} aria-live='assertive'>{errMsg}</p>
            <Input 
                placeholder='Тема задачи' 
                required={true}
                value={task?.title ?? ""}
                onChange={(event) => setTask({...task, title: event.target.value})}/>
            <Textarea 
                placeholder='Описание' 
                required={true}
                value={task?.description ?? ""}
                onChange={(event) => setTask({...task, description: event.target.value})}/>
            <NativeSelect.Root onChange={(event) => setTask({...task, executorId: event.target.value})}>
                <NativeSelect.Field>
                    <option defaultValue={undefined}>Выбрать исполнителя</option>
                    {
                        users?.map(u => (
                            <option key={u.email} value={u.id}>{u.fio}</option>
                        ))
                    }
                </NativeSelect.Field>
                <NativeSelect.Indicator />
            </NativeSelect.Root>
            <Button className='flex w-full justify-center rounded-md bg-indigo-600 px-3 py-1.5 text-sm/6 font-semibold text-white shadow-xs hover:bg-indigo-500 focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600' 
                type='submit'>
                    Создать
            </Button>
        </form>
    )
}