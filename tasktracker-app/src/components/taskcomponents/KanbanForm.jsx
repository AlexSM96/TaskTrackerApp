import { useState, useEffect } from "react";
import { Grid, GridItem, Heading } from "@chakra-ui/react";
import { fetchTasks, updateTask } from "../../services/Tasks";
import { fetchUsers } from "../../services/Users";
import KanbanTaskCard from "./KanbanTaskCard";


export default function KanbanForm(){
    const [tasks, setTasks] = useState([])
    const [users, setUsers] = useState([])
    useEffect(() => {
        const fetchData = async () => {
        let fetchedTasks = await fetchTasks()
        setTasks(fetchedTasks)
        }

        fetchData()
    }, [setTasks])

    useEffect(()=> {
          const fetchData = async () => {
            try{
              var fetchedUsers = await fetchUsers()
              setUsers(fetchedUsers)
            }
            catch(err){
              throw err;
            }
          }
      
          fetchData()
        }, [])

    const onInWorkUpdate = async (task) => {
        const response = await updateTask(task, 1)
        let fetchedTasks = await fetchTasks()
        setTasks(fetchedTasks)
    }
    
    const onExecuteUpdate = async (task) => {
        const response = await updateTask(task, 2);
        let fetchedTasks = await fetchTasks()
        setTasks(fetchedTasks)
    }

    const onUpdate = async (task) => {
      const response = await updateTask(task, task.taskWorkStatus, task.executorId)
      let fetchedTasks = await fetchTasks()
      setTasks(fetchedTasks)
    }
        
    const backlogTasks = [...tasks].filter(x => x.taskWorkStatus == 0);
    const inWorkTasks = [...tasks].filter(x => x.taskWorkStatus == 1);
    const executedTasks = [...tasks].filter(x => x.taskWorkStatus == 2);

    return (
        <div className="container mx-auto p-5 w-100">
            <Grid templateColumns="repeat(3, 1fr)" gap="3">
                <GridItem colSpan={1} bgColor={"gray.300"} variant={"elevated"}>
                   <KanbanTaskCard status={'Беклог'} users={users} filteredTasks={backlogTasks} onInWorkUpdate={onInWorkUpdate} onUpdate={onUpdate}/>
                </GridItem>
                <GridItem colSpan={1} bgColor={"gray.300"} variant={"elevated"}>
                    <KanbanTaskCard status={'В работе'} users={users} filteredTasks={inWorkTasks} onExecuteUpdate={onExecuteUpdate} onUpdate={onUpdate}/>
                </GridItem>
                <GridItem colSpan={1} bgColor={"gray.300"} variant={"elevated"}>
                    <KanbanTaskCard status={'Выполнено'} users={users} filteredTasks={executedTasks}/>
                </GridItem>
            </Grid>
        </div>
    )
}