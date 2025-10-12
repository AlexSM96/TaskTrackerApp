import React, { useRef, useState, useEffect } from 'react';
import {axiosInstance}  from "../../services/AxiosWithAuthorization"
import { useNavigate } from "react-router-dom";


const Login = () => {
  const userRef = useRef();
  const errRef = useRef();

  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [errMsg, setErrMsg] = useState('');

  useEffect(() => {
    userRef.current.focus();
  },[])

  useEffect(() => {
    setErrMsg('')
  }, [email, password])

  const navigate = useNavigate();
  const handleSubmit = async (e) => {
    e.preventDefault();
    try{
      const response = await axiosInstance.post('/accounts/login', { email, password });
      const token = response?.data?.token;
      const user = { 
        id: response?.data?.id, 
        email: response?.data?.email,
        fio: response?.data?.fio,
        roles: response?.data?.roles,
        photo: response?.data?.photo
      }

      localStorage.setItem('currentUser', JSON.stringify(user));
      localStorage.setItem('token', token);

      navigate('/tasks')
    }catch(err){
      if(!err?.response){
        setErrMsg('Сервер не отвечает')
      }else if(err.response?.status === 400){
        setErrMsg('Не введен Email или Пороль')
      }else if(err.response?.status === 401){
        setErrMsg('Вы не зарегестрированы в системе')
      }else{
        setErrMsg('Не удалось войти в систему')
      }
      errRef.current.focus();
    }
  };

  return (
    <div className="flex min-h-full flex-col justify-center px-6 py-12 lg:px-8">
      <div className="sm:mx-auto sm:w-full sm:max-w-sm">
        <h2 className="mt-10 text-center text-2xl/9 font-bold tracking-tight text-gray-900">Вход в систему в TaskTracker</h2>
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
                Войти
            </button>
          </div>
        </form>
      </div>
    </div>
  );
};


export default Login;