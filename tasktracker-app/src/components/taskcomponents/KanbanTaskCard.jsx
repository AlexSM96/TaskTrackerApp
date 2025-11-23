import { Heading } from "@chakra-ui/react"
import TaskCard from "./TaskCard"

export default function KanbanTaskCard({status, users, filteredTasks, onInWorkUpdate, onExecuteUpdate, onUpdate}){
    return (
        <>
            <Heading size={"md"} className='font-bold text-x2 grow'>{status}</Heading>
            
            <ul className='flex flex-col gap-3 w-100 m-2'>
                {filteredTasks 
                    ? filteredTasks.sort((f,s) => s.createdAt - f.createdAt).map(task => (
                        <li key={task.id}>
                            <TaskCard 
                                task={task}
                                users={users} 
                                onInWorkUpdate={onInWorkUpdate} 
                                onExecuteUpdate={onExecuteUpdate} 
                                onUpdate={onUpdate} 
                                width={"500px"}/>
                        </li>
                    ))
                    : <li><Heading>Список задач пуст</Heading></li> 
                }
            </ul>
            
        </>                    
    )
}