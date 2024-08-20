import { Component, input } from '@angular/core';
import { Memeber } from '../../_models/Memeber';

@Component({
  selector: 'app-members-card',
  standalone: true,
  imports: [],
  templateUrl: './members-card.component.html',
  styleUrl: './members-card.component.css',
})
export class MembersCardComponent {
  member = input.required<Memeber>();
}
