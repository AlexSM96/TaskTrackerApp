import { Button, Textarea } from "@chakra-ui/react"
import { useState } from "react";

export default function CreateCommentForm({taskId, onAddComment}){
    const [newComment, setNewComment] = useState(null)

    const onSubmit = (event) => {
        event.preventDefault();
        try{
            setNewComment(null)
            onAddComment(newComment)
        }
        catch(err){
           
        }
    }

    return (
        <>
            <form className='w-full flex flex-col gap-3' onSubmit={onSubmit}>
                 <Textarea 
                placeholder="Комментарий..." 
                onChange={(e) => setNewComment({...newComment, text: e.target.value, taskId: taskId}) } />
                <Button
                    className='flex w-full justify-center rounded-md bg-indigo-600 px-3 py-1.5 text-sm/6 font-semibold text-white shadow-xs hover:bg-indigo-500 focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600' 
                    type='submit'
                >Добавить комментарий</Button>
            </form>
           
        </>
    );
}