import { Heading } from "@chakra-ui/react"
import TaskCard from "./TaskCard"

export default function KanbanTaskCard({status, filteredTasks, onInWorkUpdate, onExecuteUpdate}){
    return (
        <>
            <Heading size={"md"} className='font-bold text-x2 grow'>{status}</Heading>
            <div className="m-5">
            <ul className='flex flex-col gap-3 w-100'>
                    {filteredTasks 
                        ? filteredTasks.map(task => (
                            <li key={task.id}>
                                <TaskCard task={task} onInWorkUpdate={onInWorkUpdate} onExecuteUpdate={onExecuteUpdate}/>
                            </li>
                        ))
                        : <li><Heading>Список задач пуст</Heading></li> 
                    }
                </ul>
            </div>
        </>                    
    )
}