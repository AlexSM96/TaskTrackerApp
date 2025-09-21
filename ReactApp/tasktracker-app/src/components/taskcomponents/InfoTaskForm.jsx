import { Button, CloseButton, Dialog, Portal } from "@chakra-ui/react"
import UpdateTaskForm from "./UpdateTaskForm.jsx";

export default function InfoTaskForm({task, users, onUpdate}){
    return (
        <Dialog.Root size="cover" placement="center" motionPreset="slide-in-bottom">
            <Dialog.Trigger asChild>
                <Button variant="outline" size="sm">
                &#x270e;
                </Button>
            </Dialog.Trigger>
            <Portal>
                <Dialog.Backdrop />
                <Dialog.Positioner>
                <Dialog.Content>
                    <Dialog.Header>
                    <Dialog.Title>{task.title}</Dialog.Title>
                    <Dialog.CloseTrigger asChild>
                        <CloseButton size="sm" />
                    </Dialog.CloseTrigger>
                    </Dialog.Header>
                    <Dialog.Body>
                        <UpdateTaskForm task={task} users={users} onUpdate={onUpdate}/>
                    </Dialog.Body>
                </Dialog.Content>
                </Dialog.Positioner>
            </Portal>
        </Dialog.Root>
    );
}