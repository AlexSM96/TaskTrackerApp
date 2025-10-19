import { useState, useEffect } from "react";
import { fetchOrgItems, createOrgItem, deleteOrgItem } from "../../services/OrgItems";
import CreateOrgItemForm from "../orgitemcomponents/CreateOrgItemForm"; 
import { fetchUsers } from "../../services/Users";
import DeleteButton from "../supportcomponents/DeleteButton";

const TreeNode = ({ node, onDelete }) => {
  const handleOnDelete = () => {
     onDelete(node?.id)
  }
  return (
    <li>
        <details open>
            <summary>
              {node.name}
              {node.user && (
                <span style={{ marginLeft: 8, color: 'blue' }}>({node.user.name})</span>
              )}   
              <DeleteButton onDelete={handleOnDelete} />
        </summary>
            
            {node.children && node.children.length > 0 && (
                <ul>
                    {node.children.map(child => (
                      <TreeNode key={child.id} node={child} onDelete={onDelete} />
                    ))}    
                </ul>
            )}
        </details>
    </li>
  );
};

const Tree = ({ data, onDelete }) => {
  return (
    <>
      <ul className="tree">
        {data.map(rootNode => (
          <>
            <TreeNode key={rootNode.id} node={rootNode} onDelete={onDelete} />
          </>
        ))}
      
      </ul>
    </>
    
  );
};

export default function UserForm (){
    const [orgItmes, setOrgItems] = useState([]);
    const [users, setUsers] = useState([])

    useEffect(() => {
        const fetchData = async () => {
        let fetchedOrgItems = await fetchOrgItems()
        let fetchedUsers = await fetchUsers()
        setOrgItems(fetchedOrgItems)
        setUsers(fetchedUsers)
        }

        fetchData()
    }, [setOrgItems])

    const onCreate = async (orgItem) => {
        try{
          let createdTask = await createOrgItem(orgItem)
          let fetchedOrgItems = await fetchOrgItems()
          setOrgItems(fetchedOrgItems)
        }catch(err){
          throw err;
        }
    }

    const onDelete = async (id) => {
       try{
          let isDeleted = await deleteOrgItem(id)
          let fetchedOrgItems = await fetchOrgItems()
          setOrgItems(fetchedOrgItems)
        }catch(err){
          throw err;
        }
    }

   return (
    <div className='p-8 flex flex-row justify-start items-start gap-5'>
       <div className='flex flex-col w-1/3 gap-10'>
         <CreateOrgItemForm orgItems={orgItmes} users={users} onCreate={onCreate}/>
       </div>
      <Tree data={orgItmes} onDelete={onDelete} />
    </div>
  );
}