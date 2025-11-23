export const formatDate = (date) =>{
    const currentDate = new Date(date)
    return currentDate.toLocaleDateString() + ' ' + currentDate.toLocaleTimeString();
}