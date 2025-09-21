import {axiosInstance}  from "./AxiosWithAuthorization";

export const fetchOrgItems = async () => {
    try{
        const response = await axiosInstance.get("/orgitems/get")
        return response.data
    }catch(e){
        throw e;
    }
}