import axios from "axios"

export const axiosInstance = axios.create({
    baseURL: import.meta.env.REACT_APP_API_URL || 'http://localhost:5000',
    timeout: 100000,
    headers: {"Authorization": `Bearer ${localStorage.getItem('token')}`}
});
