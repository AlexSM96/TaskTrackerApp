import React, { useRef, useState, useEffect } from 'react';
import {axiosInstance}  from '../../services/AxiosWithAuthorization';
import { useNavigate } from "react-router-dom";

const Register = ({onLogin}) => {
  const userRef = useRef();
  const errRef = useRef();

  const [username, setUsername] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [errMsg, setErrMsg] = useState('')
  const navigate = useNavigate();

  useEffect(() => {
      userRef.current.focus();
  },[])

  useEffect(() => {
    setErrMsg('')
  }, [username, email, password])

  const handleSubmit = async (e) => {
    e.preventDefault();
    try{
      const response = await axiosInstance.post('/accounts/register' , { username, email, password });
      const token = response?.data?.token;
      const user = { 
        id: response?.data?.id, 
        email: response?.data?.email,
        username: response?.data?.username,
        roles: response?.data?.roles
      }

      if(response && response.status === 200){
        onLogin(response.data);
        navigate('/tasks')
      }
      
    }catch(err){
      if(!err?.response){
        setErrMsg('Сервер не отвечает')
      }else if(err.response?.status === 400){
        setErrMsg('Не введен Email или Пороль')
      }else if(err.response?.status === 401){
        setErrMsg('Вы не зарегестрированы в системе')
      }else if(err.response?.status === 500){
        setErrMsg('Вы уже зарегестрированы')
      }
      else{
        setErrMsg('Не удалось войти в систему')
      }

      errRef.current.focus();
    }
    
  };

  return (
    <div className="flex min-h-full flex-col justify-center px-6 py-12 lg:px-8">
      <div className="sm:mx-auto sm:w-full sm:max-w-sm">
        <h1 className="mt-10 text-center text-2xl/9 font-bold tracking-tight text-gray-900"> Регистрация в системе в TaskTracker</h1>
      </div>
      <div className="mt-10 sm:mx-auto sm:w-full sm:max-w-sm">
        <p ref={errRef} className={errMsg ? "dark:text-red-400" : "offscreen"} aria-live='assertive'>{errMsg}</p>
        <form className="space-y-6" onSubmit={handleSubmit}>
          <div>
            <label className="block text-sm/6 font-medium text-gray-900">Email</label>
            <div className="mt-2">
              <input type="email" 
                name="email" 
                ref={userRef}
                autoComplete="email" 
                className="block w-full rounded-md bg-white px-3 py-1.5 text-base text-gray-900 outline-1 -outline-offset-1 outline-gray-300 placeholder:text-gray-400 focus:outline-2 focus:-outline-offset-2 focus:outline-indigo-600 sm:text-sm/6"
                onChange={(e) => setEmail(e.target.value)} 
                required/>
            </div>
          </div>
          <div>
            <label className="block text-sm/6 font-medium text-gray-900">ФИО</label>
            <div className="mt-2">
              <input type="text" 
                name="login" 
                autoComplete="login" 
                ref={userRef}
                className="block w-full rounded-md bg-white px-3 py-1.5 text-base text-gray-900 outline-1 -outline-offset-1 outline-gray-300 placeholder:text-gray-400 focus:outline-2 focus:-outline-offset-2 focus:outline-indigo-600 sm:text-sm/6"
                onChange={(e) => setUsername(e.target.value)} 
                required/>
            </div>
          </div>
          <div>
            <div className="flex items-center justify-between">
              <label className="block text-sm/6 font-medium text-gray-900">Password</label>
            </div>
            <div className="mt-2">
              <input type="password" 
                name="password"
                autoComplete="current-password" 
                className="block w-full rounded-md bg-white px-3 py-1.5 text-base text-gray-900 outline-1 -outline-offset-1 outline-gray-300 placeholder:text-gray-400 focus:outline-2 focus:-outline-offset-2 focus:outline-indigo-600 sm:text-sm/6"
                onChange={(e) => setPassword(e.target.value)} 
                required/>
            </div>
          </div>

          <div>
            <button type="submit" 
              className="flex w-full justify-center rounded-md bg-indigo-600 px-3 py-1.5 text-sm/6 font-semibold text-white shadow-xs hover:bg-indigo-500 focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600">
                Зарегистрироваться
            </button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default Register;