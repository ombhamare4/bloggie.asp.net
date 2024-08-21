import { Photo } from './Photo';

export interface Memeber {
  id: number;
  username: string;
  age: number;
  photoUrl: string;
  knownAs: string;
  created: Date;
  lastActive: Date;
  gender: string;
  introduction: string;
  interest: any;
  lookingFor: string;
  city: string;
  country: string;
  photos: Photo[];
}
