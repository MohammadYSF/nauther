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
    return response.data;
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
