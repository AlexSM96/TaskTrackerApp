import axios from "axios"

export const axiosInstance = axios.create({
    baseURL: 'https://localhost:44380/',
    timeout: 100000,
    headers: {"Authorization": `Bearer ${localStorage.getItem('token')}`}
});
