import { text } from "framer-motion/client";
import {axiosInstance}  from "./AxiosWithAuthorization";

export const fetchComments = async (taskId) => {
    try{
        const response = await axiosInstance.get(`/comments/get/${taskId}`)
        return response.data
    }catch(e){
        throw e;
    }
}

export const addComment = async (comment) => {
    try{
        if(!comment) return;
        const currentUser = JSON.parse(localStorage.getItem('currentUser'));
        const response = await axiosInstance.post('/comments/create', {
            text: comment.text,
            taskId: comment.taskId,
            authorId: currentUser.id
        })

        return response.data
    }catch(e){
        throw e;
    }
}