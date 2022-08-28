import { Component, OnInit } from '@angular/core';
import { Post } from '../models/post.model';
import { PostsService } from '../services/posts.service';

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.css']
})
export class PostsComponent implements OnInit {

  constructor(private postService: PostsService) { }

  posts: Post[] = [];

  ngOnInit(): void {
    this.postService.getAllPosts().subscribe(
      res => {
        this.posts = res;
      }
    )
  }

}
