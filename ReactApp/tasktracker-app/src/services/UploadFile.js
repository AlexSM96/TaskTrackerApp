import {axiosInstance}  from "./AxiosWithAuthorization";

export const uploadFile = async (formData) => {
    try{
        const response = await axiosInstance.post('/accounts/upload-photo', formData, {
            headers: {
                'Content-Type': 'multipart/form-data'
            }
        })

        return response.data
    }catch(e){
        throw e;
    }
}