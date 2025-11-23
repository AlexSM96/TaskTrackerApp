import { Card, Stack, Heading } from "@chakra-ui/react"
import { useEffect, useState } from "react";
import { addComment, fetchComments } from "../../services/Comments"
import { formatDate } from "../../services/DateFormat"
import CreateCommentForm from "./CreateCommentForm";

export default function TaskCommentsForm({taskId}){
    const [comments, setComments] = useState([])

    useEffect(() => {
        const fetchData = async (taskId) => {
            let fetchedComments = await fetchComments(taskId)
            setComments(fetchedComments)
        }

        fetchData(taskId)
    }, [setComments])

     const onAddComment = async (comment) => {
          try{
            let isCreated = await addComment(comment)
            if(!isCreated) return;
            let fetchedComments = await fetchComments(comment.taskId)
            setComments(fetchedComments)
          }catch(err){
            throw err;
          }
    }
    

    return (
        <>
            <br />
            <CreateCommentForm taskId={taskId} onAddComment={onAddComment} />
            <br />
            <Heading size="lg"><b>Комметарии</b></Heading>
            {comments?.sort((f,s) => s.createdAt - f.createdAt).map(c => (
                <>
                    <Stack padding={"0.5"}>
                        <Card.Root size="md" variant={"subtle"}>
                            <Card.Header>
                            <Heading size="md">От: {c?.author?.email} | {formatDate(c.createdAt)}</Heading>
                            </Card.Header>
                            <Card.Body color="fg.muted">{c.text}</Card.Body>
                        </Card.Root>
                    </Stack>
                </>
            ))}
        </>
    );
}