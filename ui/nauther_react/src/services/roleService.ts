import type { GetApiParam } from '../types/getApiParam';
import type { CreateRolePayload, CreateRoleResponseModel, DeleteRoleResponseDataModel, EditRolePayload, GetRoleByIdResponseDataModel, GetRolesResponseDataModel } from '../types/role';
import api from './api';

export const getRoles = async (
param:GetApiParam
): Promise<GetRolesResponseDataModel> => {
  let res = await api.get<GetRolesResponseDataModel>('/role', {
    params:param
  });
  return res.data;

}

export const getRoleById = async (id: string): Promise<GetRoleByIdResponseDataModel> => {
  let res = await api.get<GetRoleByIdResponseDataModel>(`/role/${id}`);
  return res.data;
}

export const createRole = async (data: CreateRolePayload): Promise<CreateRoleResponseModel> => {
  let res = await api.post<CreateRoleResponseModel>('/role', data);
  return res.data;
}

export const editRole = async (id: string, data: EditRolePayload): Promise<CreateRoleResponseModel> => {
  let res = await api.put<CreateRoleResponseModel>(`/role/${id}`, data);
  return res.data;
}

export const updateRole = async (id: string, data: CreateRolePayload): Promise<CreateRoleResponseModel> => {
  let res = await api.put<CreateRoleResponseModel>(`/role/${id}`, data);
  return res.data;
};

export const deleteRole = async (data: any) => {
  let res = await api.delete<DeleteRoleResponseDataModel>(`/role`,{data});
  return res.data;
}; 