import {
  Component,
  HostListener,
  inject,
  OnInit,
  ViewChild,
} from '@angular/core';
import { Member } from '../../_models/Member';
import { AccountService } from '../../_services/account.service';
import { MembersService } from '../../_services/members.service';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { GalleryModule, GalleryItem, ImageItem } from 'ng-gallery';
import { FormsModule, NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-member-edit',
  standalone: true,
  imports: [TabsModule, GalleryModule, FormsModule],
  templateUrl: './member-edit.component.html',
  styleUrl: './member-edit.component.css',
})
export class MemberEditComponent implements OnInit {
  @ViewChild('editForm') editForm?: NgForm;
  @HostListener('window:beforeunload', ['$event']) notify($event: any) {
    //Info: Browser Access Logic
    if (this.editForm?.dirty) {
      $event.returnValue = true;
    }
  }

  private accountService = inject(AccountService);
  private memberService = inject(MembersService);
  private toastrService = inject(ToastrService);

  member?: Member;
  images: GalleryItem[] = [];

  ngOnInit() {
    this.loadMember();
  }
  loadMember() {
    const user = this.accountService.currentUser();
    if (!user) return;
    this.memberService.getMember(user.username).subscribe({
      next: (member) => {
        this.member = member;
        member.photos.map((p) => {
          this.images?.push(
            new ImageItem({
              src: p.url,
              thumb: p.url,
            })
          );
        });
      },
    });
  }

  updateForm() {

    this.memberService.updateMember(this.editForm?.value).subscribe({
      next:_=>{
        this.toastrService.success('Profile Successfully Updated');
        this.editForm?.reset(this.member);
      }
    });
  }
}
