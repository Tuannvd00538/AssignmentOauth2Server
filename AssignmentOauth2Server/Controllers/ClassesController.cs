using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AssignmentOauth2Server.Models;

namespace AssignmentOauth2Server.Controllers
{
    public class ClassesController : Controller
    {
        private readonly AssignmentOauth2ServerContext _context;

        public ClassesController(AssignmentOauth2ServerContext context)
        {
            _context = context;
        }

        // GET: Classes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Class.ToListAsync());
        }

        // GET: Classes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var classCurrent = _context.Class.SingleOrDefault(c => c.Id == id);
            ViewData["className"] = classCurrent.Name;
            ViewData["classId"] = classCurrent.Id;

            //Get list studentByClass
            var studentClass = await _context.Classes.Where(m => m.ClassId == id).ToListAsync();
            List<AccountInfomation> accountList = new List<AccountInfomation>();
            foreach (var item in studentClass)
            {
                var account = _context.AccountInfomation.SingleOrDefault(a => a.OwnerId == item.OwnerId);
                accountList.Add(account);
            }
            ViewData["listStudent"] = accountList;

            //Get list SubjectByClass
            var subjectClass = await _context.SubjectClass.Where(c => c.ClassId == id).ToListAsync();
            ViewData["subjectClass"] = subjectClass;
            List<Subject> subjectList = new List<Subject>();
            foreach (var item in subjectClass)
            {
                var subject = _context.Subject.SingleOrDefault(a => a.Id == item.SubjectId);
                subjectList.Add(subject);
            }
            ViewData["subjectList"] = subjectList;
            return View();
            return new JsonResult(accountList);
            if (id == null)
            {
                return NotFound();
            }

            var @class = await _context.Class
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@class == null)
            {
                return NotFound();
            }

            return View(@class);
        }

        // GET: Classes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Classes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,StartTime,EndTime,IntendTime,Status")] Class @class)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@class);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@class);
        }

        // GET: Classes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @class = await _context.Class.FindAsync(id);
            if (@class == null)
            {
                return NotFound();
            }
            return View(@class);
        }

        // POST: Classes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,StartTime,EndTime,IntendTime,Status")] Class @class)
        {
            if (id != @class.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@class);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassExists(@class.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(@class);
        }

        // GET: Classes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @class = await _context.Class
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@class == null)
            {
                return NotFound();
            }

            return View(@class);
        }

        // POST: Classes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @class = await _context.Class.FindAsync(id);
            _context.Class.Remove(@class);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassExists(int id)
        {
            return _context.Class.Any(e => e.Id == id);
        }

        // GET: Classes/AddSubject/5
        public async Task<IActionResult> AddSubject(int? id)
        {
            var classCurrent = _context.Class.SingleOrDefault(c => c.Id == id);
            ViewData["className"] = classCurrent.Name;
            ViewData["classId"] = classCurrent.Id;

            var subjectClass = await _context.SubjectClass.Where(c => c.ClassId == id).ToListAsync();
            List<Subject> subjectList = new List<Subject>();
            List<int> subjectIds = new List<int>();
            foreach (var item in subjectClass)
            {
                var subject = _context.Subject.SingleOrDefault(a => a.Id == item.SubjectId);
                subjectList.Add(subject);
                subjectIds.Add(item.SubjectId);
            }
            ViewData["subjectList"] = subjectList;
            ViewData["subjectAllList"] = _context.Subject.Where(p => !subjectIds.Contains(p.Id)).ToList();

            return View();
        }

        // Post: Classes/AddSubject/5
        [HttpPost, ActionName("AddSubject")]
        public async Task<IActionResult> AddSubject(int? id, [Bind("StartTime,EndTime,IntendTime,SubjectId,ClassId")] SubjectClass sClass)
        {
            //return new JsonResult(sClass);
            if (ModelState.IsValid)
            {
                _context.Add(sClass);
                await _context.SaveChangesAsync();
                return Redirect("/Classes/Details/" + id);
                return RedirectToAction(nameof(Index));
            }

            return new JsonResult("ok");
        }

        // GET: Classes/DeleteSubject/5
        public async Task<IActionResult> DeleteSubject(int? id)
        {
            var @class = _context.SubjectClass.SingleOrDefault(s => s.Id == id); ;
            //return new JsonResult(id);
            _context.SubjectClass.Remove(@class);
            await _context.SaveChangesAsync();
            return Redirect("/Classes/Details/" + @class.ClassId);


            //if (id == null)
            //{
            //    return NotFound();
            //}
            //        var @class = await _context.SubjectClass
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (@class == null)
            //{
            //    return NotFound();
            //}
            //return Redirect("/Classes/Details/" + @class.Id);
        }

        // GET: Classes/DetailSubject/5
        public async Task<IActionResult> DetailSubject(int? id)
        {
            //return new JsonResult(stuId);

            var subjectClass = _context.SubjectClass.SingleOrDefault(c => c.Id == id);
            var subject = _context.Subject.SingleOrDefault(c => c.Id == subjectClass.SubjectId);
            var classCurrent = _context.Class.SingleOrDefault(c => c.Id == subjectClass.ClassId);
            var stuClass = _context.Classes.Where(c => c.ClassId == classCurrent.Id);
            var stuClassList = stuClass.ToList();
            var stuClassFirst = stuClass.FirstOrDefault();

            if (stuClassFirst == null)
            {
                return Redirect("/Classes/Details/" + classCurrent.Id);
            }
            else
            {
                foreach (var item in stuClassList)
                {
                    var mark = _context.MarkCurrent.Where(c => c.OwnerId == item.OwnerId).Where(c => c.SubjectClassId == id).FirstOrDefault();
                    if (mark == null)
                    {
                        Mark _mark = new Mark
                        {
                            Theory = 0,
                            Practice = 0,
                            Assignment = 0,
                            SubjectClassId = subjectClass.Id,
                            OwnerId = item.OwnerId
                        };
                        _context.MarkCurrent.Add(_mark);
                        await _context.SaveChangesAsync();
                        

                    }
                }
                var markCurrent = await _context.MarkCurrent.Where(c => c.OwnerId == stuClassFirst.OwnerId).Where(c => c.SubjectClassId == id).FirstOrDefaultAsync();
                //return new JsonResult(markCurrent);
                return Redirect("/Classes/Mark/" + markCurrent.Id);
            }


            return new JsonResult("ok");
        }
        // GET: Classes/Mark/5
        public async Task<IActionResult> Mark(int? id)
        {
            var mark = _context.MarkCurrent.SingleOrDefault(c => c.Id == id);
            var student = _context.AccountInfomation.SingleOrDefault(c => c.OwnerId == mark.OwnerId);

            var subjectClass = _context.SubjectClass.SingleOrDefault(c => c.Id == mark.SubjectClassId);
            var subject = _context.Subject.SingleOrDefault(c => c.Id == subjectClass.SubjectId);
            var classCurrent = _context.Class.SingleOrDefault(c => c.Id == subjectClass.ClassId);

            var stuList = _context.Classes.Where(c => c.ClassId == subjectClass.ClassId).ToList();
            List<AccountInfomation> accList = new List<AccountInfomation>();
            foreach(var item in stuList)
            {
                var accInfo = _context.AccountInfomation.SingleOrDefault(c => c.OwnerId == item.OwnerId);
                accList.Add(accInfo);
            }
            ViewData["listStudent"] = accList;
            ViewData["mark"] = mark;
            ViewData["stuName"] = student.FirstName + " " + student.LastName;
            ViewData["subjectName"] = subject.Name;
            ViewData["classCurrent"] = classCurrent.Name;
            //var subjectList = _context.SubjectClass.Where(c => c.ClassId == subjectClass.ClassId).ToList();
            return View(mark);
            return new JsonResult("ok");
        }

        // POST: Classes/Mark/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Mark(int? id, [Bind("Id, Theory, Practice, Assignment")] Mark mark)
        {
            //return new JsonResult(mark);
            if (id != mark.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mark);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MarktExists(mark.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(mark);
        }

        private bool MarktExists(long id)
        {
            return _context.MarkCurrent.Any(e => e.Id == id);
        }

    } 
}
