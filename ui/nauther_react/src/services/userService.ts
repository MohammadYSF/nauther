import type { GetApiParam } from '../types/getApiParam';
import type { GetAllUsersResponseDataModel, User } from '../types/user';
import api from './api';

export async function getAllExternalUsers
(param:GetApiParam)
: Promise<GetAllUsersResponseDataModel> {
    const response = await api.get<GetAllUsersResponseDataModel>('/user/external', {
        params: param,
        headers: { accept: '*/*' }
    });
    console.log("response is : ",response);
    let x = response.data.data as User[];
    return {...response.data, data: x};
}

export async function getAllUsers
(param:GetApiParam)
:Promise<GetAllUsersResponseDataModel> {
    const response = await api.get<GetAllUsersResponseDataModel>('/admin/all', {
        params: param,
        headers: { accept: '*/*' }
    });
    return response.data;
}
