import { Component, inject, OnInit } from '@angular/core';
import { MembersService } from '../../_services/members.service';
import { ToastrService } from 'ngx-toastr';
import { Memeber } from '../../_models/Memeber';
import { MembersCardComponent } from '../members-card/members-card.component';

@Component({
  selector: 'app-members-list',
  standalone: true,
  imports: [MembersCardComponent],
  templateUrl: './members-list.component.html',
  styleUrl: './members-list.component.css',
})
export class MembersListComponent implements OnInit {
  private memberService = inject(MembersService);
  private toatsr = inject(ToastrService);
  members: Memeber[] = [];

  ngOnInit(): void {
    this.loadMembers();
  }

  loadMembers() {
    this.memberService.getMembers().subscribe({
      next: (members) => (this.members = members),
      error: (error) => {},
    });
  }
}
