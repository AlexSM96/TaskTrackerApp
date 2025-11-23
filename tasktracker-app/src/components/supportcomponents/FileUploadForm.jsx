import { useState } from "react";
import { uploadFile } from "../../services/UploadFile";

export default function FileUploadForm(){
  const [file, setFile] = useState(null);

  const handleFileChange = (e) => {
    setFile(e.target.files[0]);
  };

  const handleUpload = async () => {
    if (!file) {
      alert('Пожалуйста, выберите файл');
      return;
    }

    const user = JSON.parse(localStorage.getItem('currentUser'))
    const formData = new FormData();
    formData.append('email', user.email)
    formData.append('file', file);

    try {
      const response = await uploadFile(formData);
      alert(response.message)
      setFile(null)
    } catch (error) {
      console.error('Ошибка:', error);
      alert('Произошла ошибка при отправке файла');
    }
  };

  return (
    <div>
      <input type="file" onChange={handleFileChange} />
      <button 
        className="rounded-md bg-gray-900 px-3 py-2 text-sm font-medium text-white hover:bg-red-700" 
        onClick={handleUpload}>Загрузить</button>
    </div>
  );
}