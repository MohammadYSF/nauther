import api from './api';
type CreatePermissionPayload = {
  name: string,
  displayName: string
};
type EditPermissionPayload = {
  id: string,
  name: string,
  displayName: string
};
type DeletePermissionPayload = {
  ids: string[]
};
type DeletePermissionResponseDataModel = {
  statusCode: number,
  message: string,
  validationErrors: any,
  data: {
    ids: string[]
  }
}
type CreatePermissionResponseDataModel = {
  id: string
};
type EditPermissionResponseDataModel = {
  id: string
}
type GetPermissionsResponseDataModel = {
  metadata: { total: number }
  data: {
    id: string,
    name: string,
    displayName: string
  }[]
};
type GetPermissionByIdResponseDataModel = {
  id: string,
  name: string,
  displayName: string
};

export const getPermissions = async (
  pageNumber: number = 1,
  pageSize: number = 10,
  search: string = ''
) => {
  let res = await api.get<GetPermissionsResponseDataModel>('/permission/all', {
    params: {
      pageNumber,
      pageSize,
      search,
    },
  });
  return res.data;
}
export const getPermissionById = async (id: string) => {
  let res = await api.get<GetPermissionByIdResponseDataModel>(`/permissions/${id}`);
  return res.data;
}
export const createPermission = async (data: CreatePermissionPayload) => {

  let res = await api.post<CreatePermissionResponseDataModel>('/permission', data);
  res.data;
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