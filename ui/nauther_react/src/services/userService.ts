import type { GetApiParam } from '../types/getApiParam';
import type { GetAllUsersResponseDataModel, GetAllUsersResponseDataModel_Raw, User } from '../types/user';
import api from './api';

export async function getAllExternalUsers
(param:GetApiParam)
: Promise<GetAllUsersResponseDataModel> {
    const response = await api.get<GetAllUsersResponseDataModel_Raw>('/user/external', {
        params: param,
        headers: { accept: '*/*' }
    });
    let x = JSON.parse(response.data.data);
    return {
        data:x,
        message:response.data.message,
        metadata:response.data.metadata,
        statusCode:response.data.statusCode,
        validationErrors:response.data.validationErrors,
    };
}

export async function getAllUsers
(param:GetApiParam)
:Promise<GetAllUsersResponseDataModel> {
    const response = await api.get<GetAllUsersResponseDataModel_Raw>('/admin/all', {
        params: param,
        headers: { accept: '*/*' }
    });
    let x = JSON.parse(response.data.data);
    return {
        data:x,
        message:response.data.message,
        metadata:response.data.metadata,
        statusCode:response.data.statusCode,
        validationErrors:response.data.validationErrors,
    };
}
