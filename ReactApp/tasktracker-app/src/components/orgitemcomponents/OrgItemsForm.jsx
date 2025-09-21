import { useState,useEffect } from "react";
import { fetchOrgItems } from "../../services/OrgItems";

const TreeNode = ({ node }) => {
  return (
    <li>
        <details open>
            <summary>{node.name}{node.user && (
            <span style={{ marginLeft: 8, color: 'blue' }}>- {node.user.name}</span>
            )}</summary>
            
            {node.children && node.children.length > 0 && (
                <ul>
                    {node.children.map(child => (
                        <TreeNode key={child.id} node={child} />
                    ))}
                </ul>
            )}
        </details >
    </li>
  );
};

const Tree = ({ data }) => {
  return (
    <ul className="tree">
      {data.map(rootNode => (
        <TreeNode key={rootNode.id} node={rootNode} />
      ))}
    </ul>
  );
};

export default function UserForm (){
    const [orgItmes, setOrgItems] = useState([]);

    useEffect(() => {
        const fetchData = async () => {
        let fetchedOrgItems = await fetchOrgItems()
        setOrgItems(fetchedOrgItems)
        }

        fetchData()
    }, [setOrgItems])

   return (
    <div>
      <h1>Дерево компании</h1>
      <Tree data={orgItmes} />
    </div>
  );
}