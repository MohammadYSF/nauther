
// This configuration use hybrid mode
// ServiceWorker are used if available (more secure) else tokens are given to the client
// You need to give inside your code the "access_token" when using fetch
 export const ssoServerConfig = {
  client_id: 'skoruba_identity_admin',
  redirect_uri: window.location.origin + '/signin-oidc',
  silent_login_uri: window.location.origin + '/silent-login-oidc',
  silent_redirect_uri: window.location.origin + '/silent-redirect-oidc',
  scope: 'openid profile email', // offline_access scope allow your client to retrieve the refresh_token
  authority: 'https://localhost:44310',
  // service_worker_relative_url: '/OidcServiceWorker.js', // just comment that line to disable service worker mode
  service_worker_only: false,
  demonstrating_proof_of_possession: false,
  response_type: 'code', // triggers PKCE
  token_request_extras: {
    client_secret: "skoruba_admin_client_secret",
  },
  extras: {
    client_secret: "skoruba_admin_client_secret",
  }

};