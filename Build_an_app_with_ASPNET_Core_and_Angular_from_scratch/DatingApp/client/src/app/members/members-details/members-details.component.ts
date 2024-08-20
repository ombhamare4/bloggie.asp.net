import { Component, inject, OnInit } from '@angular/core';
import { MembersService } from '../../_services/members.service';
import { ActivatedRoute } from '@angular/router';
import { Memeber } from '../../_models/Memeber';
import { TabsModule } from 'ngx-bootstrap/tabs';

@Component({
  selector: 'app-members-details',
  standalone: true,
  imports: [TabsModule],
  templateUrl: './members-details.component.html',
  styleUrl: './members-details.component.css',
})
export class MembersDetailsComponent implements OnInit {
  private memberService = inject(MembersService);
  private route = inject(ActivatedRoute);
  member?: Memeber;
  ngOnInit(): void {
    this.loadMember();
  }

  loadMember() {
    const username = this.route.snapshot.paramMap.get('username');
    console.log(username);
    if (!username) return;
    this.memberService.getMember(username).subscribe({
      next: (member) => {
        this.member = member;
      },
    });
  }
}
