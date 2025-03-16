import { Card, Heading, Text, Badge, Button } from '@chakra-ui/react'
import { formatDate } from '../../services/DateFormat'

export default function TaskCard({ task , onExecuteUpdate, onInWorkUpdate }){

    const handleInWorkUpdate = (e) => {
      onInWorkUpdate(task)
    }

    const handleExecuteUpdate = (e) => {
      onExecuteUpdate(task)
    }

    return (
      <div>
        <Card.Root variant={"elevated"}>
            <Card.Header bgColor="gray.100">
              <div className='flex'>
              <Heading size={"md"} className='font-bold text-x2 grow'>{task.title}</Heading>
               {task.executed 
                ? <Badge colorPalette={"green"}>Задача закрыта</Badge>
                : task.inWork 
                  ? <Badge colorPalette={"yellow"}>В работе</Badge> 
                  : <Badge colorPalette={"purple"}>Новая задача</Badge>
                }
               </div>
            </Card.Header>
            <Card.Body color="fg.muted" bgColor="gray.100">
              <Text>{task.description}</Text>
            </Card.Body>
            <Card.Footer bgColor="gray.100">
                <table className='w-full'>
                  <thead>
                    <tr align="left">
                      <th className='w-1/3'>Дата создания</th>
                      <th className='w-1/3'>Автор</th>
                      <th className='w-1/3'>Исполнитель</th>
                    </tr>
                  </thead>
                  <tbody>
                    <tr >
                      <td><Text textStyle={'xs'}>{formatDate(task.createdAt)}</Text></td>
                      <td><Text textStyle={'xs'}>{task.author?.username ?? '-'}</Text></td>
                      <td><Text textStyle={'xs'}>{task.executor?.username ?? '-'} </Text></td>
                    </tr>
                    {!task.inWork && !task.executed 
                      ? <tr>
                          <td colSpan={3}>
                            <Button className='flex w-full justify-center rounded-md bg-indigo-600 px-3 py-1.5 text-sm/6 font-semibold text-white shadow-xs hover:bg-indigo-500 focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600'
                                    onClick={handleInWorkUpdate}>
                              Взять в работу
                            </Button>
                          </td>
                        </tr>
                      : !task.executed 
                        ? <tr>
                            <td colSpan={3}>
                              <Button className='flex w-full justify-center rounded-md bg-green-600 px-3 py-1.5 text-sm/6 font-semibold text-white shadow-xs hover:bg-green-500 focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-green-600'
                                onClick={handleExecuteUpdate}>Выполнить
                              </Button>
                            </td>
                          </tr> 
                        : <tr></tr>
                    }
                  </tbody>
                </table>  
            </Card.Footer>
        </Card.Root>
      </div> 
    )
}