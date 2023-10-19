import {useEffect, useState} from "react";
import {get} from "../shared/AxiosHelper";
import PermissionEditor from "./PermissionEditor";
import {Permission} from "../models/Permission";
import {Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow} from "@mui/material";

const PermissionManger = () => {
    const [permissions, setPermissions] = useState<any[]>();
    const [permissionTypes, setPermissionTypes] = useState<any[]>();
    const [selectedPermission, setSelectedPermission] = useState<Permission>();
    
    const fetchPermissions = async () =>{
        const response = await get<any[]>('/api/permission');
        setPermissions(response)
    }
    const fetchPermissionTypes = async () =>{
        const response = await get<any[]>('/api/permission/type');
        setPermissionTypes(response)
    }
    
    useEffect(() => {
        fetchPermissions();
        fetchPermissionTypes();
    }, []);
    return (
        <div>
            <TableContainer component={Paper}>
                <Table sx={{ minWidth: 650 }} aria-label="simple table">
                    <TableHead>
                        <TableRow>
                            <TableCell>Id</TableCell>
                            <TableCell align="right">Name</TableCell>
                            <TableCell align="right">Last Name</TableCell>
                            <TableCell align="right">Type</TableCell>
                            <TableCell align="right">Effective from</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {permissions && permissions.map((permission) => (
                            <TableRow
                                key={permission.id}
                                onClick={()=> setSelectedPermission(permission)}
                                sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                            >
                                <TableCell component="th" scope="row">
                                    {permission.id}
                                </TableCell>
                                <TableCell align="right">{permission.name}</TableCell>
                                <TableCell align="right">{permission.lastName}</TableCell>
                                <TableCell align="right">{permission.permissionTypeDescription}</TableCell>
                                <TableCell align="right">{permission.effectiveFrom}</TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
            {permissionTypes &&
                <PermissionEditor
                    permissionTypes={permissionTypes}
                    selectedPermission={selectedPermission}
                    onSaved={()=> {
                        fetchPermissions();
                        setSelectedPermission(undefined);
                    }}
                ></PermissionEditor>
            }
            
        </div>
    );
};

export default PermissionManger;