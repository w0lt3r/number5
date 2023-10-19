import {useEffect, useState} from "react";
import {Permission} from "../models/Permission";
import { post, put} from "../shared/AxiosHelper";
import {PermissionRequest} from "../models/PermissionRequest";
import {Button, FormControl, InputLabel, MenuItem, TextField} from "@mui/material";
import Select, { SelectChangeEvent } from '@mui/material/Select';
import {DatePicker, LocalizationProvider} from "@mui/x-date-pickers";
import {AdapterDayjs} from "@mui/x-date-pickers/AdapterDayjs";
import dayjs, {Dayjs} from "dayjs";

interface PermissionEditorProps{
    permissionTypes: any[],
    selectedPermission?: Permission,
    onSaved: () => void
}

interface PermissionErrors{
    name: boolean,
    lastName: boolean,
    permissionType: boolean,
    effectiveFrom: boolean
}

const PermissionEditor = (props: PermissionEditorProps) =>{
    const [name, setName] = useState<string>('');
    const [lastName, setLastName] = useState<string>('');
    const [permissionType, setPermissionType] = useState<number | string>('');
    const [effectiveFrom, setEffectiveFrom] = useState<Dayjs | null>(null);
    const [invalidForm, setInvalidForm] = useState<boolean>(false);
    
    useEffect(()=>{
        if(props.selectedPermission){
            setName(props.selectedPermission.name);
            setLastName(props.selectedPermission.lastName);
            setPermissionType(props.selectedPermission.permissionTypeId);
            setEffectiveFrom(dayjs(props.selectedPermission.effectiveFrom));
            setInvalidForm(false);
        }
    }, [props.selectedPermission])

    const resetForm = () => {
        setName('');
        setLastName('');
        setPermissionType('');
        setEffectiveFrom(null);
        setInvalidForm(false);
    }
    
    const handlePermissionTypeChange = (newValue: number | string) =>{
        if(typeof newValue === 'number'){
            setPermissionType(newValue);
        }        
    }
    
    const onSubmit = async () =>{
        if(!name || !lastName || permissionType ==='' || effectiveFrom ===null){
            setInvalidForm(true);
            return;
        }        
        const payload = {
            name: name,
            lastName: lastName,
            permissionTypeId: permissionType,
            effectiveFrom: effectiveFrom?.toDate()
        } as PermissionRequest;
        if(props.selectedPermission){
            const response = await put<any>(`/api/permission/${props.selectedPermission.id.toString()}`,
                payload);
        }else{
            const response = await post<any>(`/api/permission`,
                payload);
        }
        resetForm();
        props.onSaved();
    }
    
    return (
        <div className={'w-full flex justify-center mt-8'}>
            <form className={'w-96'}>
                <FormControl fullWidth>
                    <TextField id="name"
                               label="Name"
                               variant="outlined"
                               value = {name}
                               onChange={(event)=> setName(event.target.value)}/>
                </FormControl>
                <FormControl fullWidth>
                    <TextField id="lastName"
                               label="Last Name"
                               variant="outlined"
                               value={lastName}
                               onChange={(event)=> setLastName(event.target.value)}/>
                </FormControl>                                
                <FormControl fullWidth>
                    <InputLabel id="demo-simple-select-label">Type</InputLabel>
                    <Select
                        labelId="demo-simple-select-label"
                        id="demo-simple-select"
                        value={permissionType}
                        label="Type"
                        onChange={(event)=> handlePermissionTypeChange(event.target.value)}
                    >
                        {props.permissionTypes.map(type =>{
                            return <MenuItem key={type.id} value={type.id}>{type.description}</MenuItem>
                        })}
                    </Select>
                </FormControl>
                <FormControl fullWidth className={'mt-8'}>
                    <LocalizationProvider dateAdapter={AdapterDayjs}>
                        <DatePicker
                            label="Effective From"
                            value={effectiveFrom}
                            onChange={(event)=> {
                                if(event){
                                    setEffectiveFrom(event);
                                    // setEffectiveFrom(new Date(event.getFullYear(),event.getMonth()+1,event.getDate()));
                                }
                            }}
                        />
                    </LocalizationProvider>                    
                </FormControl>
                <FormControl fullWidth>
                    <Button variant="contained" type={"button"} onClick={onSubmit}>Save</Button>
                </FormControl>
                {invalidForm && <span>All fields are required</span>}

            </form>
        </div>
    );
}
export default PermissionEditor;