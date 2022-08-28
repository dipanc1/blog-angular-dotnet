import { Component, OnInit } from '@angular/core';
import { AddPostRequest } from 'src/app/models/add-post.model';
import { Post } from 'src/app/models/post.model';
import { PostsService } from 'src/app/services/posts.service';

@Component({
  selector: 'app-admin-add-post',
  templateUrl: './admin-add-post.component.html',
  styleUrls: ['./admin-add-post.component.css']
})
export class AdminAddPostComponent implements OnInit {

  constructor(private postService: PostsService) { }

  post: AddPostRequest = {
    author: '',
    content: '',
    featuredImage: '',
    publishDate: '',
    summary: '',
    updateDate: '',
    title: '',
    urlHandle: '',
    visible: true
  }

  ngOnInit(): void {
  }

  onSubmit(): void{
    this.postService.addPost(this.post).subscribe(
      response => {
        alert("SUCCESS");
      }
    )
  }

}
