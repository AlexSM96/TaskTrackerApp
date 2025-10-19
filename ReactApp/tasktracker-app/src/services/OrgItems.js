import {axiosInstance}  from "./AxiosWithAuthorization";

export const fetchOrgItems = async () => {
    try{
        const response = await axiosInstance.get("/orgitems/get")
        return response.data
    }catch(e){
        throw e;
    }
}

export const createOrgItem = async (orgItem) => {
    try{
        const response = await axiosInstance.post("/orgitems/create-orgitem", {
            name: orgItem?.name,
            children: [],
            parentId: orgItem?.parent?.id,
            userId: orgItem?.user?.id
        })
        
        return response.data;
    }catch(e){
        throw e;
    }
}

export const deleteOrgItem = async (id) => {
    try{
        const response = await axiosInstance.delete(`/orgitems/delete-orgitem/${id}`)
        return response.data;
    }catch(e){
        throw e;
    }
}