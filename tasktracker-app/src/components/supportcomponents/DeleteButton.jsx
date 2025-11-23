import { Button } from "@chakra-ui/react";

export default function DeleteButton({onDelete}){
    return (
        <>
            <Button
                className="px-5 py-1 text-xs font-medium text-red-600 hover:text-red-700 hover:bg-red-50 focus-visible:outline-none w-20"
                onClick={onDelete}>
                    <img src="src\resources\remove.png" height={"50%"} width={"50%"}></img>
            </Button>
        </>
    )
}