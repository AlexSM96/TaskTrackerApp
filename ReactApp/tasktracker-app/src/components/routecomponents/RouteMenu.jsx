import { BrowserRouter as Router, Routes, Route, Link, useNavigate } from "react-router-dom"
import { Avatar } from "@chakra-ui/react"
import TaskForm from '../taskcomponents/TasksForm'
import Register from '../accountcomponents/RegisterForm'
import Login from '../accountcomponents/LoginForm'
import KanbanForm from "../taskcomponents/KanbanForm"
import OrgItemsForm from "../orgitemcomponents/OrgItemsForm"
import { axiosInstance } from "../../services/AxiosWithAuthorization"


const logout = (e) => {
  localStorage.removeItem('token');
  localStorage.removeItem('currentUser');
  delete axiosInstance.defaults.headers['Authorization'];
};

export default function RouteMunu(){
    const user = JSON.parse(localStorage.getItem('currentUser'))
    
    return (
      <Router>
        <div className="nav navbar">
          <nav className="bg-gray-800">
            <div className="mx-auto max-w-7xl px-2 sm:px-6 lg:px-8">
              <div className="relative flex h-16 items-center justify-between">
                <div className="flex flex-1 items-center justify-center sm:items-stretch sm:justify-start">
                  <div className="hidden sm:ml-6 sm:block">
                    <div className="flex space-x-4">
                      <a href="/tasks" className="rounded-md px-3 py-2 text-sm font-medium text-gray-300 hover:bg-gray-700 hover:text-white" aria-current="page">Главная страница</a>
                      <a href="/kanban" className="rounded-md px-3 py-2 text-sm font-medium text-gray-300 hover:bg-gray-700 hover:text-white">Канбан</a>
                      <a href="/orgitems" className="rounded-md px-3 py-2 text-sm font-medium text-gray-300 hover:bg-gray-700 hover:text-white">Огргструктура</a>
                    </div>
                  </div>
                  <div className="absolute right-0 z-0 mt-0 w-50 origin-top-right flex space-x-4">
                      {user 
                        ? <a className="rounded-md bg-gray-900 px-3 py-2 text-sm font-medium text-white hover:bg-gray-700">
                            <Avatar.Root size={'2xs'} key={user.email} colorPalette={'yellow'}>
                              <Avatar.Fallback name={user.email} />
                              <Avatar.Image src="#" />
                            </Avatar.Root>
                          </a>
                        : <a className="rounded-md bg-gray-900 px-3 py-2 text-sm font-medium text-white hover:bg-gray-700" href="/register">Регистрация</a>
                      }
                     
                      {user 
                        ? <a className="rounded-md bg-gray-900 px-3 py-2 text-sm font-medium text-white hover:bg-red-700" href="/login" onClick={logout}>Выйти</a> 
                        : <a className="rounded-md bg-gray-900 px-3 py-2 text-sm font-medium text-white hover:bg-gray-700" href="/login">Вход</a>
                      }
                  </div>
                </div>
              </div>
             </div>
          </nav>
          <Routes>
            <Route path="/login" element={<Login />} />
            <Route path="/register" element={<Register />} />
            <Route path="/tasks" element={<TaskForm />}/>
            <Route path="/kanban" element={<KanbanForm />}/>
            <Route path="/orgitems" element={<OrgItemsForm />} />
          </Routes>
        </div>
      </Router>
    )
  }