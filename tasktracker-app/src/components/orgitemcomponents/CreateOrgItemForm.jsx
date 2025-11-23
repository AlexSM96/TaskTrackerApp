import { useState, useRef } from "react";
import { Input, Button, NativeSelect } from '@chakra-ui/react' 

export default function CreateOrgItemForm ({orgItems, users, onCreate}){
   const [orgItem, setOrgItem] = useState(null)
   const [errMsg, setErrMsg] = useState('');
   const errRef = useRef();

   const onSubmit = (event) => {
        event.preventDefault();
        try{
            onCreate(orgItem)
        }
        catch(err){
            if(!err?.response){
                setErrMsg('Сервер не отвечает')
              }else if(err.response?.status === 401){
                setErrMsg('Вы не зарегестрированы в системе')
              }else{
                setErrMsg('Не удалось создать задачу')
              }
        }
    }

    const flattenOrgItems = (items, depth = 0) => {
        let result = []
        for (const item of items) {
            result.push({item, depth});
            if (item.children && item.children.length > 0) {
                result = result.concat(flattenOrgItems(item.children, depth + 1));
            }
        }

        return result;
    };

   return (
    <form className='w-full flex flex-col gap-3' onSubmit={onSubmit}>
        <h3 className='font-bold text-x2'>Создать элемент оргструктуры</h3>
        <p ref={errRef} className={errMsg ? "dark:text-red-400" : "offscreen"} aria-live='assertive'>{errMsg}</p>
        <Input 
            placeholder='Наименвание элемента оргструктуры' 
            required={true}
            value={orgItem?.name ?? ""}
            onChange={(event) => setOrgItem({...orgItem, name: event.target.value})}/>
        <NativeSelect.Root onChange={(event) => setOrgItem({...orgItem, parent: {id: event.target.value }})}>
            <NativeSelect.Field>
                <option defaultValue={undefined}>Выбрать родительский элемент оргструктуры</option>
                {
                    flattenOrgItems(orgItems || [])?.map(({item, depth}) => (
                       <option key={item.id} value={item.id}>{'—'.repeat(depth)} {item.name}</option>
                    ))
                }
            </NativeSelect.Field>
            <NativeSelect.Indicator />
        </NativeSelect.Root>
        
        <NativeSelect.Root onChange={(event) => setOrgItem({...orgItem, user: {id: event.target.value }})}>
            <NativeSelect.Field>
                <option defaultValue={undefined}>Выбрать сотрудника</option>
                {
                    users?.map(u => (
                        <option key={u.email} value={u.id}>{u.fio}</option>
                    ))
                }
            </NativeSelect.Field>
            <NativeSelect.Indicator />
        </NativeSelect.Root>
        <Button className='flex w-full justify-center rounded-md bg-indigo-600 px-3 py-1.5 text-sm/6 font-semibold text-white shadow-xs hover:bg-indigo-500 focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600' 
            type='submit'>
                Создать
        </Button>
    </form>
  );
}