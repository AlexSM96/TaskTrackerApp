import axios from "axios"

export const axiosInstance = axios.create({
    baseURL: 'https://localhost:7132/',
    timeout: 100000,
    headers: {"Authorization": `Bearer ${localStorage.getItem('token')}`}
});
