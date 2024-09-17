import { Component, inject, OnInit } from '@angular/core';
import { MembersService } from '../../_services/members.service';
import { ToastrService } from 'ngx-toastr';
import { Member } from '../../_models/Member';
import { MembersCardComponent } from '../members-card/members-card.component';

@Component({
  selector: 'app-members-list',
  standalone: true,
  imports: [MembersCardComponent],
  templateUrl: './members-list.component.html',
  styleUrl: './members-list.component.css',
})
export class MembersListComponent implements OnInit {
  memberService = inject(MembersService);
  private toatsr = inject(ToastrService);
  members: Member[] = [];

  ngOnInit(): void {
    if (this.memberService.members().length === 0) this.loadMembers();
  }

  loadMembers() {
    this.memberService.getMembers();
  }
}
