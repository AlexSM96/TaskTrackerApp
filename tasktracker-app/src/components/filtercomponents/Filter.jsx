import { NativeSelect, Input } from "@chakra-ui/react"

export default function Filter({filter, setFilter}){
    return (
        <div className="flex flex-col gap-5">
            <h3 className='font-bold text-x2'>Фильтры</h3>
            <Input 
                placeholder='Поиск' 
                onChange={(event) => setFilter({...filter, search: event.target.value})} />
            <NativeSelect.Root onChange={(event) => setFilter({...filter, sortOrder: event.target.value})}>
                <NativeSelect.Field>
                    <option value="desc">Сначала новые</option>
                    <option value="asc">Сначала старые</option>
                </NativeSelect.Field>
                <NativeSelect.Indicator />
            </NativeSelect.Root>
            <NativeSelect.Root onChange={(event) => setFilter({...filter, taskStatus: event.target.value})}>
                <NativeSelect.Field>
                    <option value={''}>Все</option>
                    <option value={1}>В работе</option>
                    <option value={2}>Закрытые задачи</option>
                </NativeSelect.Field>
                <NativeSelect.Indicator />
            </NativeSelect.Root>
        </div>
    );
}