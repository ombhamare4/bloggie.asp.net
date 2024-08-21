import { Component, inject, OnInit } from '@angular/core';
import { MembersService } from '../../_services/members.service';
import { ActivatedRoute } from '@angular/router';
import { Memeber } from '../../_models/Memeber';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { GalleryModule, GalleryItem, ImageItem } from 'ng-gallery';

@Component({
  selector: 'app-members-details',
  standalone: true,
  imports: [TabsModule, GalleryModule],
  templateUrl: './members-details.component.html',
  styleUrl: './members-details.component.css',
})
export class MembersDetailsComponent implements OnInit {
  private memberService = inject(MembersService);
  private route = inject(ActivatedRoute);
  member?: Memeber;
  images: GalleryItem[] = [];

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
}
