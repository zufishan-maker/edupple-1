export class AuthModel {
  access_token: string;
  refresh_token: string;
  expires_in: string;

  setAuth(auth: any) {
    this.access_token = auth.accessToken;
    this.refresh_token = auth.refreshToken;
    this.expires_in = auth.expiresIn;
  }
}
