import type { CreateAdminPayload, CreateAdminResponseDataModel, DeleteAdminPayload, DeleteAdminResponseDataModel, EditAdminPayload, EditAdminResponseDataModel, GetAdminByIdResponseDataModel, GetAdminsResponseDataModel } from '../types/admin';
import type { GetApiParam } from '../types/getApiParam';
import api from './api';

export const getAdmins = async (
 param:GetApiParam
): Promise<GetAdminsResponseDataModel> => {
  var res = await api.get<GetAdminsResponseDataModel>('https://mocki.io/v1/93df223d-3b7c-406c-adca-ae747117119d', {
    params: param
  });
  return res.data;
}

export const getAdminById = async (id: string): Promise<GetAdminByIdResponseDataModel> => {
  let res = await api.get<GetAdminByIdResponseDataModel>(`/admin/detail?id=${id}`);
  return res.data;
}
export const createAdmin = async (data: CreateAdminPayload): Promise<CreateAdminResponseDataModel> => {
  let res = await api.post<CreateAdminResponseDataModel>('/admin', data);
  return res.data;
}
export const editAdmin = async (id: string, data: EditAdminPayload): Promise<EditAdminResponseDataModel> => {
  let res = await api.put<EditAdminResponseDataModel>(`/admin`, data);
  return res.data;
} 
export const deleteAdmin = async ( data: DeleteAdminPayload): Promise<DeleteAdminResponseDataModel> => {
  let res = await api.delete<DeleteAdminResponseDataModel>(`/admin`,{data:data});
  return res.data;
} 
