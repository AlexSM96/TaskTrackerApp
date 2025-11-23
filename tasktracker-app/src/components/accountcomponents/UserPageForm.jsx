import { Grid, GridItem, Image } from '@chakra-ui/react';
import FileUploadForm from '../supportcomponents/FileUploadForm';
import { fetchUsers } from '../../services/Users';
import { useEffect, useState } from 'react';
import TaskCard from '../taskcomponents/TaskCard';
import { fetchTasks } from '../../services/Tasks';

export default function UserPageForm() {
  const currentUser = JSON.parse(localStorage.getItem('currentUser'));
  if (!currentUser) {
    return <p>Пожалуйста, авторизуйтесь.</p>;
  }

  const [user, setUser] = useState({});
  const [allUsers, setUsers] = useState([]);
  const [userTasks, setUserTasks] = useState([])
  useEffect(() => {
    const fetchData = async () => {
        const users = await fetchUsers();

        const curr = users.filter(x => x.id === currentUser.id)[0];
        setUser(curr);
        setUsers(users);
        const tasks = await fetchTasks()
        setUserTasks(tasks)
    }

    fetchData()
  }, [])
 
 

  return (
    <div className="container mx-auto p-5 w-full">
      <Grid templateColumns="repeat(3, 1fr)" gap={6}>
        <GridItem colSpan={3} bgColor="gray.100" borderRadius="2xl" p={6} boxShadow="lg">
          <h1 className="text-3xl font-semibold mb-4">Личный кабинет</h1>
          <div className="flex items-center space-x-6">
            <Image
                src={user.photo}
                boxSize="150px"
                borderRadius="full"
                objectFit="cover"
                alt={user.name}
            />
            
            <div>
              <h2 className="text-xl font-semibold mb-2">{user.fio}</h2>
              <p className="text-gray-600 mb-4">{user.email}</p>
              <div className="space-x-3">
                <FileUploadForm />
              </div>
            </div>
          </div>
        </GridItem>
        <GridItem colSpan={3} bgColor="gray.100" borderRadius="2xl" p={6} boxShadow="lg">
          <h1 className="text-3xl font-semibold mb-4">Текущие задачи</h1>
          <div className="flex items-center space-x-6">
             <ul className='flex flex-col gap-5 w-2/3'>
                {userTasks 
                    ? userTasks.filter(t => t?.taskWorkStatus === 1 && t?.executor?.id === user.id).map(task => (
                        <li key={task.id}>
                            <TaskCard task={task} users={allUsers} width={'150%'}/>
                        </li>
                    ))
                    : ( <li><Heading>Список задач пуст</Heading></li> ) 
                }
             </ul>
            
          </div>
        </GridItem>
      </Grid>
    </div>
  );
}