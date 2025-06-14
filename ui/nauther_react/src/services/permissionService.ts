import type { GetApiParam } from '../types/getApiParam';
import type { CreatePermissionPayload, CreatePermissionResponseDataModel, DeletePermissionResponseDataModel, EditPermissionPayload, EditPermissionResponseDataModel, GetPermissionByIdResponseDataModel, GetPermissionsResponseDataModel, Permission } from '../types/permission';
import api from './api';

export const getPermissions = async (
param:GetApiParam
) => {
  let res = await api.get<GetPermissionsResponseDataModel>('/permission', {
    params: param
  });
  return res.data;
}
export const getPermissionById = async (id: string) => {
  let res = await api.get<GetPermissionByIdResponseDataModel>(`/permission/${id}`);
  return res.data;
}
export const createPermission = async (data: CreatePermissionPayload) => {

  let res = await api.post<CreatePermissionResponseDataModel>('/permission', data);
  return res.data;
}
export const editPermission = async (data: EditPermissionPayload) => {
  let res = await api.put<EditPermissionResponseDataModel>(`/permission/${data.id}`, data);
  return res.data;
}
export const deletePermissions = async (data: any) => {
  let res = await api.delete<DeletePermissionResponseDataModel>(`/permission`, { data });
  return res.data;
};
export type {
  EditPermissionResponseDataModel, CreatePermissionResponseDataModel, CreatePermissionPayload,
  EditPermissionPayload,
  GetPermissionByIdResponseDataModel, GetPermissionsResponseDataModel
}