import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Memeber } from '../_models/Memeber';

@Injectable({
  providedIn: 'root',
})
export class MembersService {
  private http = inject(HttpClient);
  baseUrl = environment.apiUrl;

  getMembers() {
    return this.http.get<Memeber[]>(this.baseUrl + 'users');
  }
  getMember(username: string) {
    return this.http.get<Memeber>(this.baseUrl + 'users/' + username);
  }
}
