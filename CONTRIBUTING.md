# Contributing

Contributions to this project are [released](https://help.github.com/articles/github-terms-of-service/#6-contributions-under-repository-license) to the public under the [project's open source license](LICENSE).

Everyone is welcome to contribute to this project. Contributing doesn't just mean submitting pull requests—there are many different ways for you to get involved, including answering questions, reporting issues, improving documentation, or suggesting new features.

## How to Contribute

### Reporting Issues

If you find a bug or have a feature request:
1. Check if the issue already exists in the GitHub Issues
2. If not, create a new issue with:
   - Clear title and description
   - Steps to reproduce (for bugs)
   - Expected vs actual behavior
   - Your environment details (OS, .NET version, SQL Server version)

### Submitting Pull Requests

1. Fork the repository
2. Create a new branch for your feature/fix:
   ```bash
   git checkout -b feature/your-feature-name
   ```
3. Make your changes following the code style guidelines below
4. Test your changes thoroughly
5. Commit with clear, descriptive messages
6. Push to your fork and submit a pull request

### Code Style Guidelines

This project uses:
- **ASP.NET Web Forms** (.NET Framework 3.5)
- **C#** for server-side logic
- **LINQ-to-SQL** for database operations
- **SQL Server** for data persistence

Before submitting:
- Ensure your code compiles without errors
- Test all database operations
- Verify web handlers (.ashx) work correctly
- Check that regex patterns for email extraction are accurate

### Coding Standards

1. **Email validation**: All email extraction logic must include proper validation
2. **Database operations**: Use proper locking mechanisms for concurrent operations
3. **Error handling**: Use try-catch blocks appropriately, avoid empty catch blocks when possible
4. **Naming**: Use clear, descriptive names for variables and methods
5. **Comments**: Add comments for complex regex patterns and business logic

### Adding New Features

When adding new features:
1. Update the relevant classes in `App_Code/`
2. Create or modify web handlers (.ashx) or pages (.aspx)
3. Update database schema if needed (LINQ-to-SQL mapping)
4. Test with actual search engines (be mindful of rate limiting)
5. Update configuration in `web.config` if necessary

### Database Schema

When modifying the database:
1. Update the SQL Server database schema
2. Regenerate the LINQ-to-SQL mapping (`.designer.cs` files)
3. Test all database operations
4. Document schema changes

## Security Considerations

**Important**: This application scrapes data from public websites and stores email addresses. When contributing:
- Do not include actual database connection strings in commits
- Do not commit real email addresses or personal data
- Be mindful of web scraping ethics and rate limiting
- Ensure compliance with anti-spam regulations (CAN-SPAM, GDPR)

## Questions or Need Help?

Please feel free to contact me with any question, comment, pull-request, issue, or any other thing you have in mind.

* Or Assayag <orassayag@gmail.com>
* GitHub: https://github.com/orassayag
* StackOverflow: https://stackoverflow.com/users/4442606/or-assayag?tab=profile
* LinkedIn: https://linkedin.com/in/orassayag

Thank you for contributing! 🙏
