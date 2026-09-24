

using CLI.UI;
using InMemoryRepositories;
using RepositoryContracts;
using Entities;

Console.WriteLine("Starting CLI application...");
IUserRepository userRepository = new UserInMemoryRepository();
ICommentRepository commentRepository = new CommentInMemoryRepository();
IPostRepository postRepository = new PostInMemoryRepository();
IForumRepository forumRepository = new ForumInMemoryRepository();
IReactionRepository interactionRepository = new ReactionInMemoryRepository();
await userRepository.AddAsync(new User("alice", "password123"));
await userRepository.AddAsync(new User("bob", "hunter2"));

CliApp app = new CliApp(userRepository, commentRepository, postRepository, forumRepository, interactionRepository);
await app.StartAsync();
