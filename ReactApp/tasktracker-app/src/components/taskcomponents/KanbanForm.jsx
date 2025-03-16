import { useState, useEffect } from "react";
import { Grid, GridItem, Heading } from "@chakra-ui/react";
import { fetchTasks, updateTask } from "../../services/Tasks";
import KanbanTaskCard from "./KanbanTaskCard";


export default function KanbanForm(){
    const [tasks, setTasks] = useState([])
    useEffect(() => {
        const fetchData = async () => {
        let fetchedTasks = await fetchTasks()
        setTasks(fetchedTasks)
        }

        fetchData()
    }, [setTasks])

    const onInWorkUpdate = async (task) => {
        const response = await updateTask(task, true, null)
        let fetchedTasks = await fetchTasks()
        setTasks(fetchedTasks)
    }
    
    const onExecuteUpdate = async (task) => {
        const response = await updateTask(task, false, true);
        let fetchedTasks = await fetchTasks()
        setTasks(fetchedTasks)
    }
        
    const backlogTasks = [...tasks].filter(x => !x.inWork && !x.executed);
    const inWorkTasks = [...tasks].filter(x => x.inWork && !x.executed);
    const executedTasks = [...tasks].filter(x => x.executed);

    return (
        <div className="container mx-auto p-5 w-100">
            <Grid templateColumns="repeat(3, 1fr)" gap="5">
                <GridItem colSpan={1} bgColor={"gray.300"} variant={"elevated"}>
                   <KanbanTaskCard status={'Беклог'} filteredTasks={backlogTasks} onInWorkUpdate={onInWorkUpdate}/>
                </GridItem>
                <GridItem colSpan={1} bgColor={"gray.300"}>
                    <KanbanTaskCard status={'В работе'} filteredTasks={inWorkTasks} onExecuteUpdate={onExecuteUpdate}/>
                </GridItem>
                <GridItem colSpan={1} bgColor={"gray.300"}>
                    <KanbanTaskCard status={'Выполнено'} filteredTasks={executedTasks}/>
                </GridItem>
            </Grid>
        </div>
    )
}