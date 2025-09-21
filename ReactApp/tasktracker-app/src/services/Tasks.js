import {axiosInstance}  from "./AxiosWithAuthorization";

export const fetchTasks = async (filter) => {
    try{
        const response = await axiosInstance.get("/tasks/get", {
            params: {
                title: filter?.search,
                sortItem: filter?.sortItem,
                sortOrder: filter?.sortOrder,
                taskStatus: filter?.taskStatus
            }
        })

        return response.data
    }catch(e){
        throw e;
    }
}

export const createTask = async (task) => {
    try{
        const currentUser = JSON.parse(localStorage.getItem('currentUser'));
        const response = await axiosInstance.post("/tasks/create", {
            title: task.title,
            description: task.description,
            authorId: currentUser.id,
            executorId: task.executorId
        })

        return response.data;
    }catch(e){
       throw e;
    }
}

export const updateTask = async (task, executorId, inWork, executed) => {
    try{
        const currentUser = JSON.parse(localStorage.getItem('currentUser'));
        const response = await axiosInstance.put("/tasks/update", {
            id: task.id,
            title: task.title,
            description: task.description,
            authorId: task.author.id,
            executed: executed,
            executorId: executorId ?? currentUser.id,
            currentUserId: currentUser.id,
            inWork: inWork,
            startWorkDate: task.startWorkDate,
            endWorkDate: task.endWorkDate
        })

        return response.data;
    }catch(e){
        throw e;
    }
}