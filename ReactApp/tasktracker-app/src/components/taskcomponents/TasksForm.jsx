import { useState, useEffect } from "react";
import Filter from "../filtercomponents/Filter";
import CreateTaskForm from "./CreateTaskForm";
import TaskCard from "./TaskCard";
import { fetchTasks, createTask, updateTask } from '../../services/Tasks'
import { Heading } from "@chakra-ui/react";

export default function TasksForm (){
    const [tasks, setTasks] = useState([])
    const [filter, setFilter] = useState({
      search: "",
      sortItem: "date",
      sortOrder: "desc"
    })

    useEffect(() => {
      const fetchData = async () => {
        try{
          let fetchedTasks = await fetchTasks(filter)
          setTasks(fetchedTasks)
        }
        catch(err){
          throw err;
        }
        
      }
  
      fetchData()
    }, [filter])
  
    const onCreate = async (task) => {
      try{
        let createdTask = await createTask(task)
        let fetchedTasks = await fetchTasks(filter)
        setTasks(fetchedTasks)
      }catch(err){
        throw err;
      }
    }

    const onInWorkUpdate = async (task) =>{
          const response = await updateTask(task, true, null)
          let fetchedTasks = await fetchTasks(filter)
          setTasks(fetchedTasks)
        }
    
    const onExecuteUpdate = async (task) => {
      const response = await updateTask(task, false, true);
      let fetchedTasks = await fetchTasks(filter)
      setTasks(fetchedTasks)
    }
    

    return (
        <div className='p-8 flex flex-row justify-start items-start gap-12'>
            <div className='flex flex-col w-1/3 gap-10'>
                <CreateTaskForm onCreate={onCreate}/>
                <Filter filter={filter} setFilter={setFilter}/>
            </div>
            <ul className='flex flex-col gap-5 w-2/3'>
                {tasks 
                  ? tasks.map(task => (
                        <li key={task.id}>
                            <TaskCard task={task} onInWorkUpdate={onInWorkUpdate} onExecuteUpdate={onExecuteUpdate} />
                        </li>
                    ))
                  : <li><Heading>Список задач пуст</Heading></li> 
                }
            </ul>
        </div>
    )
}
