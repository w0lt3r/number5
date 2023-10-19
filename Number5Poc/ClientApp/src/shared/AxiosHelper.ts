import axios, {AxiosError} from "axios";
import {HttpRequestError} from "../models/HttpRequestError";
export const post = async <Output>(url: string, data: any) => {
    try{
        const response = await axios.post<Output>(url,
            data);
        return response.data;
    }catch (ex: any){
        if(ex instanceof  AxiosError){
            throw new HttpRequestError(
                ex.response?.status == 500 ? 'An unexpected error has ocurred. Please, try again later.'
                    : ex.response?.data
            )
        }else{
            throw new HttpRequestError(
                'An unexpected error has ocurred. Please, try again later.'
            )
        }
    }
}

export const put = async <Output>(url: string, data: any) => {
    try{
        const response = await axios.put<Output>(url,
            data);
        return response.data;
    }catch (ex: any){
        if(ex instanceof  AxiosError){
            throw new HttpRequestError(
                ex.response?.status == 500 ? 'An unexpected error has ocurred. Please, try again later.'
                    : ex.response?.data
            )
        }else{
            throw new HttpRequestError(
                'An unexpected error has ocurred. Please, try again later.'
            )
        }
    }
}

export const get = async <Output>(url: string) => {
    try{
        const response = await axios.get<Output>(url);
        return response.data;
    }catch (ex: any){
        if(ex instanceof  AxiosError){
            throw new HttpRequestError(
                ex.response?.status == 500 ? 'An unexpected error has ocurred. Please, try again later.'
                    : ex.response?.data
            )
        }else{
            throw new HttpRequestError(
                'An unexpected error has ocurred. Please, try again later.'
            )
        }
    }
} 