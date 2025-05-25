import axios from 'axios';
import api from './api';

export interface User {
    id: string;
    userCode: string;
    profileImage: string;
    username: string;
    phoneNumber: string;
    isActive: boolean;
    permissions:{id:string,displayName:string}[],
    roles:{id:string,displayName:string}[]
}

interface GetAllUsersResponseDataModel_Raw {
    statusCode: number;
    message: string;
    validationErrors: any;
    data: {
        key: string;
        value: string; // JSON string
    }[];
    metadata: { total: number }
}
export interface GetAllUsersResponseDataModel {
    statusCode: number;
    message: string;
    validationErrors: any;
    data: User[];
    metadata: { total: number }
}

export async function getAllExternalUsers(pageNumber: number, pageSize: number, search: string): Promise<GetAllUsersResponseDataModel> {
    const response = await api.get<GetAllUsersResponseDataModel_Raw>('/user/external', {
        params: { pageNumber, pageSize, search },
        headers: { accept: '*/*' }
    });
    let x = response.data.data.map(item => ({
        ...JSON.parse(item.value),
        key: item.key,
    } as User));
    return {...response.data, data: x};
}

export async function getAllUsers(pageNumber: number, pageSize: number, search: string): Promise<GetAllUsersResponseDataModel> {
    const response = await api.get<GetAllUsersResponseDataModel>('/user/all', {
        params: { pageNumber, pageSize, search },
        headers: { accept: '*/*' }
    });
    return response.data;
}
