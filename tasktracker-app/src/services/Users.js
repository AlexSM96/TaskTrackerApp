import {axiosInstance}  from "./AxiosWithAuthorization";

export const fetchUsers = async () => {
    try{
        const response = await axiosInstance.get("/accounts/getusers")
        return response.data
    }catch(e){
        throw e;
    }
}